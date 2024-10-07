const mongoose = require('mongoose');

const campusSchema = new mongoose.Schema({
    address: String,
    room: String,
    floor: Number
});

const Campus = mongoose.model('Campus', campusSchema);

module.exports = Campus