from fastapi import APIRouter, UploadFile, File, Form
from starlette.datastructures import Headers

from services.gemma_chat_bot.business_logic.chat_bot import GemmaChatBot, user_histories

gemma_chat_bot_router=APIRouter()

@gemma_chat_bot_router.post("/gemma-chat-bot")

async def gemma_chat_bot(text:str=Form(...),
                         user_id:str=Form(...),
                         file:UploadFile=File(default=UploadFile(filename='', size=0, headers=Headers({'content-disposition': 'form-data; name="file"; filename=""', 'content-type': '',}),file=b""))):
    if user_id not in user_histories:
        user_histories[user_id] = []
    result=await GemmaChatBot.gemma_bot(file=file,text=text,user_id=user_id)
    print(result)
    return {"response":result}
