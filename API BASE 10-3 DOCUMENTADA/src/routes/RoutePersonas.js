const express = require('express');
const router = express.Router();
const personas = require('../models/PersonasModel');

//create People

/** 
 * @swagger    
 * components:
 *  schemas:
 *      Personas:
 *          type: object
 *          properties:
 *              nombre:
 *                  type: string
 *                  description: Nombre de la persona
 *              apellido:
 *                  type: string
 *                  description: Apellido de la persona
 *              dni:
 *                  type: integer
 *                  description: DNI de la persona
 *              telefono:
 *                  type: string
 *                  description: Telefono de la persona
 *              email:
 *                  type: string
 *                  description: Email de la persona
 *              domicilio:
 *                  type: string
 *                  description: Domicilio de la persona
 *              nacimiento:
 *                  type: string
 *                  description: Fecha de nacimiento de la persona
 *          required:
 *              - nombre
 *              - apellido
 *              - dni
 *              - telefono
 *              - email
 *              - domicilio
 *              - nacimiento
 *          example:
 *              nombre: 
 *              apellido:
 *              dni:
 *              telefono:
 *              email:
 *              domicilio:
 *              nacimiento:
 *              
 */

/**
 * @swagger
 * /api/personas:
 *  get:
 *      summary: Obtener todas las personas de la BD
 *      tags: [Personas]
 *      responses:
 *          200:
 *              description: OK!  
 *              content: 
 *                  application/json:
 *                      schema:
 *                          type: array
 *                          items:
 *                              $ref: '#/components/schemas/Personas'
 *                             
 *                               
 */

router.get('/', async function (req, res) {

   await  personas.personas.findAll({ attributes: ['id','nombre','apellido'] })
        .then(personas => {
            res.json(personas)
        })
        .catch(err => {
            console.log(err)
        })

})

/**
 * @swagger
 * /api/personas/{id}:
 *  get:
 *      summary: Obtener una persona de la BD
 *      tags: [Personas]
 *      parameters:
 *          -   in: path
 *              name: id
 *              schema:
 *                  type: string    
 *              required: true
 *              description: Ingrese el ID de la persona   
 *      responses:
 *          200:
 *              description: OK!  
 *              content: 
 *                  application/json:
 *                      schema:
 *                          type: object
 *                          $ref: '#/components/schemas/Personas'
 *      404:
 *          description:Persona no encontrada en la BD
 *                             
 *                               
 */

router.get('/:id', async function (req,res) 
{
    const {id} = req.params;
await personas.personas.findByPk(id,{attributes:[ 'id','nombre','apellido','dni','telefono','email','domicilio','nacimiento']})
.then(personas => {
    res.json(personas)
})
.catch(err => {
    console.log(err)
})
});

/**
 * @swagger
 * /api/personas:
 *  post:
 *      summary: Crear una persona en la BD
 *      tags: [Personas]
 *      requestBody:
 *          required: true
 *          content:
 *              application/json:
 *                  schema:
 *                      type: object
 *                      $ref: '#/components/schemas/Personas'
 *      responses:
 *          200:
 *              description: UNA NUEVA PERSONA HA SIDO CREADA!
 */


router.post('/', async function (req,res) 
{
  const {nombre,apellido,dni,telefono,email,domicilio,nacimiento} = req.body;
    await personas.personas.create
    (
      { nombre:nombre,
        apellido:apellido,
        dni:dni,
        telefono:telefono,
        email:email,
        domicilio:domicilio,
        nacimiento:nacimiento
      })
  .then(personas => {
    res.json(personas)
})
.catch(err => {
    console.log(err)
});

});


/**
 * @swagger
 * /api/personas/{id}:
 *  put:
 *      summary: Actualizar una persona en la BD
 *      tags: [Personas]
 *      parameters:
 *          -   in: path
 *              name: id
 *              schema:
 *                  type: string    
 *              required: true
 *              description: Ingrese el ID de la persona 
 *      requestBody:
 *          required: true
 *          content:
 *              apllication/json:
 *                  schema:
 *                      type: object
 *                      $ref: '#/components/schemas/Personas'
 *      responses:
 *          200:
 *              description: UNA PERSONA HA SIDO ACTUALIZADA!
 *          400:
 *              description: Persona no encontrada en la BD
 */


router.put('/:id', async function (req,res) 
{
    const {id} = req.params;
    const {nombre,apellido,dni,telefono,email,domicilio,nacimiento} = req.body;
    
    await personas.personas.update
    (
        {
        nombre:nombre,
        apellido:apellido,
        dni:dni,
        telefono:telefono,
        email:email,
        domicilio:domicilio,
        nacimiento:nacimiento
        },
        {
            where: {
                id: id
            }
        }
    )
    .then(personas => {
        res.json(personas)
    })
    .catch(err => {
        console.log(err)
    });


})

/**
 * @swagger
 * /api/personas/{id}:
 *  delete:
 *      summary: Obtener una persona de la BD
 *      tags: [Personas]
 *      parameters:
 *          -   in: path
 *              name: id
 *              schema:
 *                  type: string    
 *              required: true
 *              description: Ingrese el ID de la persona   
 *      responses:
 *          200:
 *              description: La persona ha sido eliminada de la BD...  
 *          404:
 *              description:Persona no encontrada en la BD
 *                             
 *                               
 */


router.delete('/:id', async function (req, res) {
    const {id} = req.params;
     await personas.personas.destroy({
      where: {
          id: id
      }
  })
  .then(res.send(`El ${id} se borro correctamente`)
  )
});




module.exports = router;