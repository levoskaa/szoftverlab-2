import { Component, OnInit, OnDestroy } from '@angular/core';
import { Room, User, Message } from '../../models';
import { HubBuilderService } from 'src/app/services/hub-builder.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit, OnDestroy {
  activeTab: 'rooms' | 'peeps' = 'peeps';

  rooms: Room[];
  peeps: User[];

  newRoomName: string;
  newRoomIsPrivate: boolean = false;
  newRoomPasskey: string;

  lobbyMessages: Message[];
  lobbyLoading: boolean = false;

  chatMessage: string;
  connection: signalR.HubConnection;

  constructor(hubBuilder: HubBuilderService, private router: Router) {
    this.connection = hubBuilder.getConnection();
    // Beregisztráljuk a szervertől érkező üzenetek eseménykezelőjét. Típusosan is tudnánk kezelni egy
    // olyan objektum tulajdonságainak bejárásával, aminek tulajdonságai az eseménykezelők.
    this.connection.on("SetUsers", users => this.setUsers(users));
    this.connection.on("UserEntered", user => this.userEntered(user));
    this.connection.on("UserLeft", userId => this.userLeft(userId));
    this.connection.on("SetMessages", messages => this.setMessages(messages));
    this.connection.on("ReceiveMessage", message => this.receiveMessage(message));
    this.connection.on("RoomCreated", room => this.roomCreated(room));
    this.connection.on("RoomAbandoned", roomName => this.roomAbandoned(roomName));
    this.connection.on("SetRooms", rooms => this.setRooms(rooms));
    // TODO: További eseménykezelőket is kell majd beregisztrálnunk itt.
    this.peeps = [];
    this.rooms = [];
    this.lobbyMessages = [];
    this.connection.start().then(() => {
      this.connection.invoke("EnterLobby");
    });
  }

  ngOnInit() { }

  ngOnDestroy() {
    // Amikor a komponens megsemmisül (pl. navigáció esetén), zárjuk a kapcsolatot. Ne felejtsük el az
    // eseménykezelőket leiratkoztatni, különben memory leakünk lesz!
    this.connection.off("SetUsers");
    this.connection.off("UserEntered");
    this.connection.off("UserLeft");
    this.connection.off("SetMessages");
    this.connection.off("ReceiveMessage");
    this.connection.off("RoomCreated");
    this.connection.off("RoomAbandoned");
    this.connection.off("SetRooms");
    // TODO: A később felregisztrált eseménykezelőket is itt iratkoztassuk le!
    this.connection.stop(); // A stop() függvény valójában aszinkron, egy Promise-szal tér vissza. A
    // kapcsolat lebontása időt vesz igénybe, de nem használjuk újra a connection objektumot, ezért
    // nem okoz gondot, ha néhány másodpercig még élni fog az az obj
  }

  receiveMessage(message: Message) {
    // A szerver új üzenet érkezését jelzi:
    this.lobbyMessages.splice(0, 0, message);
  }

  userEntered(user: User) {
    // A szerver azt jelezte, hogy az aktuális szobába csatlakozott egy user. Ezt el kell
    // tárolnunk a felhasználókat tároló tömbben.
    this.peeps.push(user);
  }

  userLeft(userId: string) {
    // A szerver azt jelezte, hogy a megadott ID-jú felhasználó elhagyta a szobát, így ki kell
    // vennünk a felhasználót a felhasználók tömbjéből ID alapján.
    this.peeps = this.peeps.filter((user) => user.id !== userId);
  }

  setUsers(users: User[]) {
    // A szerver belépés után leküldi nekünk a teljes user listát:
    this.peeps = users;
  }

  setMessages(messages: Message[]) {
    // A szerver belépés után leküldi nekünk a korábban érkezett üzeneteket:
    this.lobbyMessages = messages;
  }

  sendMessage() {
    // A szervernek az invoke függvény meghívásával tudunk üzenetet küldeni.
    this.connection.invoke("SendMessageToLobby", this.chatMessage);
    // A kérés szintén egy Promise, tehát feliratkoztathatnánk rá eseménykezelőt, ami akkor sül el, ha
    // a szerver jóváhagyta a kérést (vagy esetleg hibára futott). A szerver egyes metódusai Task
    // helyett Task<T>-vel is visszatérhetnek, ekkor a válasz eseménykezelőjében megkapjuk a válasz
    // objektumot is:
    // this.connection.invoke("SendMessageToLobby", this.chatMessage)
    // .then((t: T) => {
    // console.log(t);
    // })
    // .catch(console.error);
    this.chatMessage = "";
  }

  createRoom() {
    // Szoba létrehozása szerveren, majd navigáció a szoba útvonalára, szükség esetén megadni a passkey-t
    this.connection.invoke("CreateRoom", this.newRoomName).then((room: Room) => {
      this.enterRoom(room);
    });
  }

  roomCreated(room: Room) {
    // Szobalista frissítése
    this.rooms.push(room);
  }

  roomAbandoned(roomName: string) {
    // Szobalista frissítése
    this.rooms = this.rooms.filter((room) => room.name !== roomName);
  }

  enterRoom(room: Room) {
    // TODO: navigáció a szoba útvonlára, figyelve, hogy kell-e megadni passkey-t
    this.router.navigate([`/room/${room.name}`]);
  }

  setRooms(rooms: Room[]) {
    this.rooms = rooms;
  }
}
