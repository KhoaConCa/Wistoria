const WebSocket = require('ws'); // Import the WebSocket library

// Create a new WebSocket server instance
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('Server is running on port 8080');
});

wss.on('connection', (ws) => {
    ws.on('message', (data) => {
        console.log('data received %o' + data)
        ws.send(data);
    })
})

wss.on('listening', () => {
    console.log('Server is listening on port 8080');
})