# utils.py
import re
from pathlib import Path
import nltk
from nltk.corpus import stopwords
from nltk.tokenize import word_tokenize

nltk.download("punkt")
nltk.download("stopwords")

skills_file = Path("data/skills_list.txt")
with skills_file.open("r", encoding="utf-8") as f:
    known_skills = set(line.strip().lower() for line in f if line.strip())

STOP_WORDS = set(stopwords.words("english"))

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
    filtered = [t for t in tokens if t not in STOP_WORDS and len(t) > 1]
    return " ".join(filtered)

def extract_skills_from_resume(resume_text: str) -> str:
    cleaned = clean_resume(resume_text)
    tokens = cleaned.split()
    matched = {tok for tok in tokens if tok in known_skills}
    return " ".join(sorted(matched))
