const mongoose = require('mongoose');

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

module.exports = studentSchema