const mongoose = require('mongoose');

const datalogSchema = new mongoose.Schema({
    printer_id: { type: String, required: true },
    document_id: { type: mongoose.Schema.Types.ObjectId, ref: "Document", required: true},
    start_at: { type: Date, required: true},
    end_at: { type: Date, required: true}
});

module.exports = datalogSchema