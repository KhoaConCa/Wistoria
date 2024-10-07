const mongoose = require('mongoose');
const bankAccountSchema = require('./bankAccount');

const paymentSchema = new mongoose.Schema({
    username: { type: mongoose.Schema.Types.ObjectId, ref: "Account", required: true },
    purchase_date: { type: Date, required: true },
    number_of_page: { type: Number, required: true },
    cost: { type: Number, required: true },
    bank_account: bankAccountSchema,
    status: { type: String, default: "Paid", required: true }
});

module.exports = paymentSchema