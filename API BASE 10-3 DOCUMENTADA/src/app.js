const express = require('express');
const sequelize = require('./database/db');
const app = express();
const path = require('path');

const port = process.env.port || 3000


//SWAGGER

const swaggerUI = require('swagger-ui-express');
const swaggerJsDoc = require('swagger-jsdoc');
const swaggerSpec = 
{

  definition: 

  { openapi: "3.0.0",
    info: 
    {
      title: "WEB API VENTAS NODE JS",
      version: "1.0.0"

    },

    servers:
    [
      {
        url:"http://localhost:3000"
        
      }
    ]

    

  
  },

  apis: [`${path.join(__dirname,"./routes/*.js")}`]


}




app.use(express.urlencoded({extended: false}));
app.use(express.json());
app.use('/api-doc',swaggerUI.serve,swaggerUI.setup(swaggerJsDoc(swaggerSpec)))

app.use('/api/ventas', require('./routes/ventas'));
app.use('/api/articulos', require('./routes/articulos'));
app.use('/api/personas', require('./routes/RoutePersonas'));


app.listen(port, () => {
  console.log(`App listening on port ${port}`)

  sequelize.authenticate({force:true})
  .then(()=>{
      console.log("Conexion ok");
  });
});
