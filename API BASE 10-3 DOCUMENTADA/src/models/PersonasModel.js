const { Sequelize, Model } = require('sequelize');

const variable = require('../database/db')



const personas = variable.define('personas', {
    id: { type: Sequelize.SMALLINT, primaryKey: true,allowNull:false,autoIncrement:true },
    nombre:{type: Sequelize.STRING, unique: true, allowNull: false},
    apellido: Sequelize.STRING,
    dni: Sequelize.BIGINT,
    telefono: Sequelize.STRING,
    email: Sequelize.STRING,
    domicilio: Sequelize.STRING,
    nacimiento: Sequelize.DATE,

});


module.exports.personas = personas;

