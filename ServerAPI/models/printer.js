const mongoose = require('mongoose');
const campusSchema = require('./campus');

const printerSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    type: { type: String, required: true },
    description: String,
    locate_at: campusSchema,
    status: { type: String, default: "Online", required: true }
});

module.exports = printerSchema