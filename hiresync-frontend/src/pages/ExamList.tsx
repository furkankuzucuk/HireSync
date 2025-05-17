import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { ExamDto } from '../types/ExamTypes';

const ExamList = () => {
    const [exams, setExams] = useState<ExamDto[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        axios.get('/api/exams')
            .then(response => setExams(response.data))
            .catch(error => console.error('Error fetching exams:', error));
    }, []);

    const handleStartExam = (id: number) => {
        navigate(`/worker-dashboard/exam/${id}`);
    };

    return (
        <div>
            <h2>Available Exams</h2>
            <ul>
                {exams.map(exam => (
                    <li key={exam.examId}>
                        {exam.examName} - {new Date(exam.examDate).toLocaleDateString()}
                        <button onClick={() => handleStartExam(exam.examId)} style={{ marginLeft: '1rem' }}>
                            Sınava Başla
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ExamList;
