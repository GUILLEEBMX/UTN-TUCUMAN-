const {Model, DataTypes} = require('sequelize');
const sequelize = require('../database/db');

class Venta extends Model{}
Venta.init({
    id: {type: DataTypes.SMALLINT,
        primaryKey: true,
        allowNull: true,
        autoIncrement: true
    },
    comprador: DataTypes.INTEGER,
    vendedor: DataTypes.INTEGER,
    fecha: DataTypes.DATE,
    factura: DataTypes.STRING,
},{
    sequelize,
    modelName: "ventas"
});

module.exports= Venta;