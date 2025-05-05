// src/pages/JobDetails.tsx
import React, { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import axios from "axios";

const JobDetails = () => {
  const { id } = useParams();
  const [job, setJob] = useState<any>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchJob = async () => {
      try {
        const response = await axios.get(`http://localhost:5065/api/joblists/${id}`);
        setJob(response.data);
      } catch (error) {
        console.error("İlan detayı alınamadı:", error);
      }
    };
    fetchJob();
  }, [id]);

  const handleApplyClick = () => {
    navigate(`/register?jobListId=${id}`);
  };

  if (!job) return <div className="container mt-5">Yükleniyor...</div>;

  return (
    <div className="container mt-5">
      <div className="card shadow-lg border-0">
        <div className="card-header bg-primary text-white text-center py-3">
          <h3 className="mb-0">{job.departmentName}</h3>
        </div>
        <div className="card-body p-4">
          <h5 className="card-title">Pozisyon Açıklaması</h5>
          <p className="card-text">{job.description}</p>
          <p className="text-muted">Yayın Tarihi: {new Date(job.createDate).toLocaleDateString()}</p>

          <button className="btn btn-success me-2" onClick={handleApplyClick}>
            Başvur
          </button>
          <Link to="/" className="btn btn-secondary">Geri Dön</Link>
        </div>
      </div>
    </div>
  );
};

export default JobDetails;
