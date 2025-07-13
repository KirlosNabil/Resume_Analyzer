from pydantic import BaseModel
from typing import List

class ResumeAIRequest(BaseModel):
    resume: str

class PredictedJob(BaseModel):
    title: str
    confidence: float

class ResumeAIResponse(BaseModel):
    predicted_jobs: List[PredictedJob]
    skills: List[str]