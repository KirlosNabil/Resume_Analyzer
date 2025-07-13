from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

class ResumeInput(BaseModel):
    resume: str

@app.post("/match")
def match_resume(data: ResumeInput):
    return {"message": "Resume received successfully"}