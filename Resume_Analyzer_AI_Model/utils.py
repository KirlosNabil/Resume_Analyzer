import re
from pathlib import Path
import nltk
from nltk.corpus import stopwords
from nltk.tokenize import sent_tokenize, word_tokenize
from sentence_transformers import SentenceTransformer, util

#nltk.download("punkt")
#nltk.download("stopwords")

skills_file = Path("data/skills_list.txt")
with skills_file.open("r", encoding="utf-8") as f:
    known_skills = [line.strip() for line in f if line.strip()]

stop_words = set(stopwords.words("english"))
model = SentenceTransformer("all-MiniLM-L6-v2")

skill_embeddings = model.encode(known_skills, convert_to_tensor=True)

def normalize_spacing(text: str) -> str:
    text = re.sub(r'(?<=[a-z0-9])(?=[A-Z])', ' ', text)
    text = re.sub(r'(?<=[A-Za-z])(?=\d)', ' ', text)
    text = re.sub(r'(?<=\d)(?=[A-Za-z])', ' ', text)
    text = re.sub(r'([^\w\s])', r' \1 ', text)
    return re.sub(r'\s+', ' ', text).strip()

def clean_resume(text: str) -> str:
    text = normalize_spacing(text)
    text = text.lower()
    text = re.sub(r'[^\w\s]', ' ', text)
    text = re.sub(r'\d+', ' ', text)
    tokens = word_tokenize(text)
    filtered = [t for t in tokens if t not in stop_words and len(t) > 1]
    return " ".join(filtered)

def extract_skills_from_resume(resume_text: str) -> list:
    threshold = 0.9
    candidate_phrases = list(set(resume_text.split()))
    candidate_phrases = [p for p in candidate_phrases if p.lower() not in stop_words and len(p) > 1]
    phrase_embeddings = model.encode(candidate_phrases, convert_to_tensor=True)
    extracted_skills = set()
    for idx, phrase_embedding in enumerate(phrase_embeddings):
        similarity = util.cos_sim(phrase_embedding, skill_embeddings)
        max_sim_idx = similarity.argmax().item()
        max_score = similarity[0][max_sim_idx].item()
        if max_score >= threshold:
            extracted_skills.add(known_skills[max_sim_idx])
    return sorted(extracted_skills)
