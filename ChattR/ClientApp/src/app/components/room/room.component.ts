import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Room, User } from 'src/app/models';
import { HubBuilderService } from 'src/app/services/hub-builder.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  id: string;
  peeps: User[];
  roomMessages: Message[];
  roomLoading: boolean = false;
  chatMessage: string;
  connection: signalR.HubConnection;

  constructor(
    private route: ActivatedRoute,
    hubBuilder: HubBuilderService,
    private router: Router
  ) {
    route.params.subscribe(p => {
      this.id = p["id"];
    });
    this.connection = hubBuilder.getConnection();
    // Beregisztráljuk a szervertől érkező üzenetek eseménykezelőjét. Típusosan is tudnánk kezelni egy
    // olyan objektum tulajdonságainak bejárásával, aminek tulajdonságai az eseménykezelők.
    this.connection.on("SetUsers", users => this.setUsers(users));
    this.connection.on("UserEntered", user => this.userEntered(user));
    this.connection.on("UserLeft", userId => this.userLeft(userId));
    this.connection.on("SetMessages", messages => this.setMessages(messages));
    this.connection.on("ReceiveMessage", message => this.receiveMessage(message));
    // TODO: További eseménykezelőket is kell majd beregisztrálnunk itt.
    this.peeps = [];
    this.roomMessages = [];
    this.connection.start().then(() => {
      this.connection.invoke("EnterRoom", this.id);
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
    // TODO: A később felregisztrált eseménykezelőket is itt iratkoztassuk le!
    this.connection.stop(); // A stop() függvény valójában aszinkron, egy Promise-szal tér vissza. A
    // kapcsolat lebontása időt vesz igénybe, de nem használjuk újra a connection objektumot, ezért
    // nem okoz gondot, ha néhány másodpercig még élni fog az az obj
  }

  receiveMessage(message: Message) {
    // A szerver új üzenet érkezését jelzi:
    this.roomMessages.splice(0, 0, message);
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
    this.roomMessages = messages;
  }

  sendMessage() {
    // A szervernek az invoke függvény meghívásával tudunk üzenetet küldeni.
    this.connection.invoke("SendMessageToRoom", this.id, this.chatMessage);
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

  enterRoom(room: Room) {
    // TODO: navigáció a szoba útvonlára, figyelve, hogy kell-e megadni passkey-t
    this.router.navigate([`/room/${room.name}`]);
  }
}
