
const { Router } = require('express');
const router = new Router();


const Articulo = require('../models/articulo');

router.get('/', (req, res) => {
    Articulo.findAll({attributes: ['id','nombre','descripcion',
      ]})
      .then(articulos =>{
       res.json(articulos);
      })
      .catch(error=> {
        console.log(error)
      });
});


router.post('/', async (req,res) =>{

const {nombre, descripcion, stock, codigo, marca, precio} = req.body;

await Articulo.create({
    nombre: nombre,
    descripcion: descripcion,
    stock: stock,
    codigo: codigo,
    marca: marca,
    precio: precio
})
.then(articulos =>{
res.json(articulos)
})
.catch(error =>{
    console.log(error)
})
});



module.exports = router;