from fastapi import FastAPI
from utils import extract_skills_from_resume
from schemas import ResumeAIRequest, ResumeAIResponse, PredictedJob
import  pickle

with open("model/model.pkl", "rb") as f:
    model = pickle.load(f)

with open("model/vectorizer.pkl", "rb") as f:
    vectorizer = pickle.load(f)

with open("model/label_encoder.pkl", "rb") as f:
    label_encoder = pickle.load(f)

app = FastAPI()


@app.post("/match", response_model=ResumeAIResponse)
def match_resume(data: ResumeAIRequest):
    skills_str = extract_skills_from_resume(data.resume)
    if not skills_str:
        return {"predicted_jobs": [], "skills": []}
    vector = vectorizer.transform([skills_str])
    pred = model.predict(vector)[0]
    conf = max(model.predict_proba(vector)[0])
    title = label_encoder.inverse_transform([pred])[0]
    return {
        "predicted_jobs": [PredictedJob(title=title, confidence=round(conf,2))],
        "skills": skills_str.split()
    }
