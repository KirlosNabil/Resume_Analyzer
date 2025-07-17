from fastapi import FastAPI
from utils import extract_skills_from_resume
from schemas import ResumeAIRequest, ResumeAIResponse, PredictedJob
import pickle
import numpy as np

with open("model/model.pkl", "rb") as f:
    model = pickle.load(f)

with open("model/vectorizer.pkl", "rb") as f:
    vectorizer = pickle.load(f)

with open("model/label_encoder.pkl", "rb") as f:
    label_encoder = pickle.load(f)

app = FastAPI()

@app.post("/match", response_model=ResumeAIResponse)
def match_resume(data: ResumeAIRequest):
    skills = extract_skills_from_resume(data.resume)
    skills_str = " ".join(skills)
    if not skills_str:
        return {"predicted_jobs": [], "skills": []}
    vector = vectorizer.transform([skills_str])
    probs = model.predict_proba(vector)[0]
    top_indices = np.argsort(probs)[::-1][:3]
    predicted_jobs = [
        PredictedJob(
            title=label_encoder.inverse_transform([i])[0],
            confidence=round(probs[i], 2)
        )
        for i in top_indices
    ]
    return {
        "predicted_jobs": predicted_jobs,
        "skills": skills
    }