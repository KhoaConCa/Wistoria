//#region Import library

const express = require('express')
const mongoose = require('mongoose')
const bodyParser = require('body-parser')
const cors = require('cors')

//#endregion

//#region Init

const app = express()
const port = 3000

//#region Middleware
app.use(bodyParser.json())
app.use(cors())
//#endregion

//#region Connect to MongoDB
const MONGODBURL = "mongodb+srv://dnk270204:Khoa270204@wistoria.la6uu.mongodb.net/unity?retryWrites=true&w=majority&appName=Wistoria"

mongoose.connect(MONGODBURL)
    .then(() => {
        console.log("Connected to MongoDB");
    })
    .catch((err) => {
        console.log(err);
})
//#endregion

//#endregion

const printerRouter = require('./routes/printer')

app.use('/printers', printerRouter)

app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
})