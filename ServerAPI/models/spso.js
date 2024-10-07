const mongoose = require('mongoose');

const spsoSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    phone_number: { type: String, required: true },
    email: { type: String, required: true },
    account_id: { type: mongoose.Schema.Types.ObjectId, ref: "Account", required: true},
    status: { type: String, default: "Working", required: true }
});

module.exports = spsoSchema