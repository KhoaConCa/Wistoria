const mongoose = require('mongoose');

const documentSchema = new mongoose.Schema({
    id: { type: String, required: true, unique: true },
    name: { type: String, required: true },
    paper_type: { type: String, required: true },
    side: { type: String, required: true },
    copy: { type: Number, required: true },
    student_id: { type: mongoose.Schema.Types.ObjectId, ref: "Student", required: true},
    status: { type: String, default: "Waiting", required: true }
});

module.exports = documentSchema