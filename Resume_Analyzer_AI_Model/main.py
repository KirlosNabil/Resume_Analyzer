from fastapi import FastAPI
from pydantic import BaseModel
from utils import clean_resume

app = FastAPI()

class ResumeInput(BaseModel):
    resume: str

@app.post("/match")
def match_resume(data: ResumeInput):
    cleaned = clean_resume(data.resume)
    return {"cleaned_text": cleaned}