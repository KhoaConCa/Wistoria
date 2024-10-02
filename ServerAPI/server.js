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

//#region Schema and model

//#region Campus 
const campusSchema = new mongoose.Schema({
    address: String,
    room: String,
    floor: Number
});

const Campus = mongoose.model("Campus", campusSchema)
//#endregion

//#region Printer
const printerSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    type: { type: String, required: true },
    description: String,
    locate_at: campusSchema,
    status: { type: String, default: "Online", required: true }
});

const Printer = mongoose.model("Printer", printerSchema)
//#endregion

//#region Account
const accountSchema = new mongoose.Schema({
    username: { type: String, required: true, unique: true },
    password: { type: String, required: true },
    role: { type: String, required: true },
    paper_left: { type: Number, required: true }
});

const Account = mongoose.model("Account", accountSchema)
//#endregion

//#region SPSO
const spsoSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    phone_number: { type: String, required: true },
    email: { type: String, required: true },
    account_id: { type: mongoose.Schema.Types.ObjectId, ref: "Account", required: true},
    status: { type: String, default: "Working", required: true }
});

const SPSO = mongoose.model("SPSO", spsoSchema)
//#endregion

//#region Student
const studentSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    birth_day: { type: Date },
    phone_number: { type: String, required: true },
    email: { type: String, required: true },
    class: { type: String, required: true },
    course: { type: String, required: true },
    account_id: { type: mongoose.Schema.Types.ObjectId, ref: "Account", required: true},
    status: { type: String, default: "Studying", required: true }
});

const Student = mongoose.model("Student", studentSchema)
//#endregion

//#region Document
const documentSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    paper_type: { type: String, required: true },
    side: { type: String, required: true },
    copy: { type: Number, required: true },
    student_id: { type: mongoose.Schema.Types.ObjectId, ref: "Student", required: true},
    status: { type: String, default: "Waiting", required: true }
});

const Document = mongoose.model('Document', documentSchema);
//#endregion

//#region Datalog
const datalogSchema = new mongoose.Schema({
    printer_id: { type: String, required: true },
    document_id: { type: mongoose.Schema.Types.ObjectId, ref: "Document", required: true},
    start_at: { type: Date, required: true},
    end_at: { type: Date, required: true}
});

const Datalog = mongoose.model("Datalog", datalogSchema)
//#endregion

//#region Config
const configSchema = new mongoose.Schema({
    default_page: { type: Number, required: true },
    update_day: { type: Date, required: true },
    spso_id: { type: mongoose.Schema.Types.ObjectId, ref: "SPSO", required: true },
    status: { type: String, default: "Using", required: true }
});

const Config = mongoose.model("Config", configSchema)
//#endregion

//#region Bank Account
const bankAccountSchema = new mongoose.Schema({
    account_id: { type: String, required: true , unique: true },
    bank_name: { type: String, required: true },
    status: { type: String, default: "Using", required: true }
});

const BankAccount = mongoose.model("BankAccount", bankAccountSchema)
//#endregion

//#region Payment
const paymentSchema = new mongoose.Schema({
    username: { type: mongoose.Schema.Types.ObjectId, ref: "Account", required: true },
    purchase_date: { type: Date, required: true },
    number_of_page: { type: Number, required: true },
    cost: { type: Number, required: true },
    bank_account: bankAccountSchema,
    status: { type: String, default: "Paid", required: true }
});

const Payment = mongoose.model("Payment", paymentSchema)
//#endregion


app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
})