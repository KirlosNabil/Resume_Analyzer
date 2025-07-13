from fastapi import FastAPI
from pydantic import BaseModel
from utils import clean_resume
from schemas import ResumeAIRequest, ResumeAIResponse, PredictedJob

app = FastAPI()

@app.post("/match", response_model=ResumeAIResponse)
def match_resume(data: ResumeAIRequest):
    cleaned = clean_resume(data.resume)
    return {"cleaned_text": cleaned}