const mongoose = require('mongoose');

const bankAccountSchema = new mongoose.Schema({
    account_id: { type: String, required: true , unique: true },
    bank_name: { type: String, required: true },
    status: { type: String, default: "Using", required: true }
});

module.exports = bankAccountSchema