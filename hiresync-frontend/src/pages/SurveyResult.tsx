import React, { useEffect, useState } from 'react';
import { getSurveyResults } from '../services/SurveyService';

interface SurveyResult {
  QuestionText: string;
  AnswerDistribution: { [key: string]: number };  // Adjust this based on your actual API structure
}

interface SurveyResultsProps {
  surveyId: number;
}

const SurveyResult: React.FC<SurveyResultsProps> = ({ surveyId }) => {
  const [results, setResults] = useState<SurveyResult[]>([]);

  useEffect(() => {
    const fetchResults = async () => {
      try {
        const data = await getSurveyResults(surveyId);
        setResults(data);
      } catch (error) {
        alert('Error fetching survey results.');
      }
    };
    fetchResults();
  }, [surveyId]);

  return (
    <div>
      <h3>Survey Results</h3>
      {results.length > 0 ? (
        results.map((result, index) => (
          <div key={index}>
            <h4>{result.QuestionText}</h4>
            <ul>
              {Object.entries(result.AnswerDistribution).map(([answer, count]) => (
                <li key={answer}>{answer}: {count} votes</li>
              ))}
            </ul>
          </div>
        ))
      ) : (
        <p>No results yet.</p>
      )}
    </div>
  );
};

export default SurveyResult;
