const signalR = require("@aspnet/signalr");

XMLHttpRequest = require("xmlhttprequest").XMLHttpRequest;
WebSocket = require("websocket").w3cwebsocket;

var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:80/stocks")
    .build();

hubConnection.start()
    .then(() => {
        hubConnection.stream("StreamStocks").subscribe({
            next: (stock) => {
                console.log(stock);
                console.log(stock.Symbol + " " + stock.Price);
            },
            error: (err) => { console.error(err);},
            complete: () => { console.log("StreamStocks completed.");}
        });
    });