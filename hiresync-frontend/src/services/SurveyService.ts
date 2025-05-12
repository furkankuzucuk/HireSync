import axios from 'axios';

const API_URL = 'http://localhost:5065/api'; // Backend URL

// Define the type for surveyData (you should adjust this type based on the actual structure of your data)
interface SurveyData {
  answers: Array<{
    questionId: number;
    answer: string;
  }>;
}

// Anketi gönderme
export const submitSurveyAnswers = async (userId: number, surveyData: SurveyData): Promise<any> => {
  try {
    const response = await axios.post(`${API_URL}/surveyanswers/submit`, surveyData, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('authToken')}`
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error submitting survey:", error);
    throw error;
  }
};

// Anket sonuçlarını getirme
export const getSurveyResults = async (surveyId: number): Promise<any> => {
  try {
    const response = await axios.get(`${API_URL}/surveyanswers/results/${surveyId}`, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('authToken')}`
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error getting survey results:", error);
    throw error;
  }
};

// Anket sorularını almak
export const getSurveyQuestions = async (surveyId: number): Promise<any> => {
    try {
      const response = await axios.get(`${API_URL}/surveyquestions/survey/${surveyId}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching survey questions:', error);
      throw error;
    }
};
