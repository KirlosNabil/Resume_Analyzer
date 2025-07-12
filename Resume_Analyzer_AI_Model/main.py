from fastapi import FastAPI

app = FastAPI()

@app.post("/match")
def match_resume():
    return {"message": "Resume received successfully"}