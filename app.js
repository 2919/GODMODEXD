const Discord = require('discord.js')
const client = new Discord.Client()
client.login(process.env.TOKEN)

client.on('message', message => {
  var prefix = '-'
  if(message.content.toLowerCase() === `${prefix}version`){
    
     message.channel.send('``` SHAWTje DISCORD BOT VERSION - 1.0 ```')
     



  }

  
  
  
 
  
  })
