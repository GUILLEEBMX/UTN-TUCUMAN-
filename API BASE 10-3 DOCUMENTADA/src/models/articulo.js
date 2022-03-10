const {Model, DataTypes} = require('sequelize');
const sequelize = require('../database/db');

class Articulo extends Model{}
Articulo.init({
    id: {type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true,
        allowNull:false,
        },
    nombre: DataTypes.STRING,
    descripcion: DataTypes.STRING,
    stock: DataTypes.INTEGER,
    codigo: DataTypes.STRING,
    marca: DataTypes.STRING,
    precio: {type:DataTypes.DECIMAL,
        allowNull:false},
},
{
    sequelize,
    modelName: "articulos"
});

module.exports= Articulo;