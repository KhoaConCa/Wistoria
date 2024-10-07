const express = require('express');
const router = express.Router();
const Campus = require('../models/campus');

router.post('/', async (req, res) => {
    const campus = new Campus(req.body);
    try{
        const newCampus = await campus.save();
        res.status(201).json(newCampus);
    } catch (err) {
        res.status(400).json({ message: err.message });
    }
});

router.get('/', async (req, res) => {
    try{
        const campuses = await Campus.find();
        res.json(campuses);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

router.get('/:id', async (req, res) => {
    try{
        const campuses = await Campus.findById(req.params.id);      

        if(campuses == null) {
            return res.status(404).json({ message: "Cannot find campus" });
        }

        res.json(campuses);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

router.delete('/:id', async (req, res) => {
    try{
        const campuses = await Campus.findById(req.params.id);

        if(campuses == null) {
            return res.status(404).json({ message: "Cannot find campus" });
        }   

        await campuses.remove();
        res.json({ message: "Deleted Campus" });
    } catch(err) {
        res.status(500).json({ message: err.message });
    }
});

module.exports = router;

