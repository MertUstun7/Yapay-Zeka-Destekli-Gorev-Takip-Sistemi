import uvicorn
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from services.gemma_chat_bot.presentation.routing import gemma_chat_bot_router

app=FastAPI()


app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

app.include_router(gemma_chat_bot_router,tags=["gemma-chat-bot"])

if __name__=="__main__":
    uvicorn.run(app,host="127.0.0.1",port=8012)