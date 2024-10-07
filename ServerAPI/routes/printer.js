const express = require('express');
const router = express.Router();
const Printer = require('../models/printer');

router.post('/printer', async (req, res) => {
    const printer = new Printer(req.body);
    try{
        const newPrinter = await printer.save();
        res.status(201).json(newPrinter);
    } catch (err) {
        res.status(400).json({ message: err.message });
    }
});

router.get('/', async (req, res) => {
    try{
        const printers = await Printer.find();
        res.json(printers);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

router.get('/printer/:id', async (req, res) => {
    try{
        const printers = await Printer.findById(req.params.id);

        if(printers == null) {
            return res.status(404).json({ message: "Cannot find printer" });
        }

        res.json(printers);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

router.patch('/printer/:id', async (req, res) => {
    try{
        const printer = await Printer.findById(req.params.id);

        if(printer == null) {
            return res.status(404).json({ message: "Cannot find printer" });
        }

        res.json(printer);
    } catch(err) {
        res.status(400).json({ message: err.message });
    }
});

router.delete('/printer/:id', async (req, res) => {
    try{
        const printer = await Printer.findById(req.params.id);

        if(printer == null) {
            return res.status(404).json({ message: "Cannot find printer" });
        }

        await printer.remove();
        res.json({ message: "Deleted Printer" });
    } catch(err) {
        res.status(500).json({ message: err.message });
    }
});

module.exports = router;