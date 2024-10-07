const mongoose = require('mongoose');

const configSchema = new mongoose.Schema({
    default_page: { type: Number, required: true },
    update_day: { type: Date, required: true },
    spso_id: { type: mongoose.Schema.Types.ObjectId, ref: "SPSO", required: true },
    status: { type: String, default: "Using", required: true }
});

module.exports = configSchema