const mongoose = require('mongoose');

const campusSchema = new mongoose.Schema({
    address: String,
    room: String,
    floor: Number
});

module.exports = campusSchema