import json
import os

from dotenv import load_dotenv
from google import genai
import fitz
load_dotenv()
user_histories = {}
class GemmaChatBot:
    def __int__(self):
        pass
    @staticmethod
    async def read_pdf_file(file):

        with fitz.open(stream=file, filetype="pdf") as doc:
            text = ""
            for page in doc:
                text += page.get_text()
            return text

    @staticmethod
    async def gemma_bot(file,text,user_id):

        if file.filename!="":
           file= await file.read()
           context=await GemmaChatBot.read_pdf_file(file=file)
        else:
            context=""

        prompt={
            "file_context":context,
            "client":text
        }
        if context=="":
          del prompt["file_context"]
          prompt_context=""
        else:
            prompt_context="'file_context' alanında verilen bağlamı dikkate alarak kullanıcı isteklerine yerine getir."


        prompt=json.dumps(prompt)
        user_histories[user_id].append({"role":"user","parts":[prompt]})
        client = genai.Client(api_key=os.getenv("GEMMA"))
        response = client.models.generate_content(
            model="gemma-3n-e4b-it",
            contents="Sen şirket işlemlerinde şirket sahipleri ve yöneticilere iş süreçlerinde yardımcı olan bir yapay zeka asistanısın."
                     "Kullanıcılar seninle sohbet edebilir ve iş süreçlerinde yardımını isteyebilirler."
                     "Yazı dili olarak bir istek talep edildiğinde resmi bir dil kullan. Sohbet etmek için kullandığında arkadaşı gibi yazabilirsin."
                     "Kullanıcıların taleplerine yerine getir."
                      +prompt_context
                      +str(user_histories[user_id]))
        user_histories[user_id].append({"role":"model","parts":[response.text]})


        return response.text


