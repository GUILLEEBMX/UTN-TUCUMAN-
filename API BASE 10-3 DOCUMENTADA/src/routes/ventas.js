
const { Router } = require('express');
const router = new Router();

const Venta = require('../models/venta');

router.get('/', async (req, res) => {
    
    await Venta.findAll({attributes: ['id','comprador','vendedor',
      ]})
      .then(ventas =>{
       res.json(ventas);
      })
      .catch(error=> {
        console.log(error)
      });
});


router.get('/:id', async (req, res) => {
    const { id } = req.params;
    await Venta.findByPk(id,{attributes: ['id','comprador','vendedor','fecha', 'factura']}) 
      .then(venta =>{
       res.json(venta);
      })
      .catch(error=> {
        console.log(error)
      });
});

router.post('/',async (req, res) => {
  const{ comprador,vendedor,fecha,factura}= req.body;
  await Venta.create({
        comprador:comprador,
        vendedor: vendedor,
        fecha: fecha,
        factura: factura
  })
  .then(venta=>{
    res.json(venta)
  })
});

router.put('/:id', async (req, res) => {
    const{id}= req.params
    const{ comprador,vendedor,fecha,factura}= req.body;

     await Venta.update({
        comprador:comprador,
        vendedor: vendedor,
        fecha: fecha,
        factura: factura
    }, {
        where: {
            id: id
        }
    })
    .then(res.send(`http://localhost:3000/api/ventas/${id}`)
    );
});

router.delete('/:id', async (req, res) => {
    const {id} = req.params;
     await Venta.destroy({
      where: {
          id: id
      }
  })
  .then(res.send(`El ${id} se borro correctamente`)
  )
});


module.exports = router;