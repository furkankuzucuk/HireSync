import React, { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import axios from "axios";
import "../css/JobDetails.css";

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
        console.error("İlan detayları alınamadı:", error);
      }
    };
    fetchJob();
  }, [id]);

  const handleApplyClick = () => {
    navigate(`/register?jobListId=${id}`);
  };

  if (!job)
    return (
      <div className="container py-5 text-center text-muted">
        <div className="spinner-border text-primary" role="status"></div>
        <p className="mt-3">Yükleniyor...</p>
      </div>
    );

  return (
    <div className="job-details container py-5">
      <div className="card shadow border-0 animate-fade">
        <div className="card-header job-header text-center py-4">
          <h3 className="mb-0">{job.title}</h3>
        </div>
        <div className="card-body p-4">
          <div className="job-meta mb-3">
            <p><strong>📁 Departman:</strong> {job.departmentName}</p>
            <p><strong>👔 Pozisyon:</strong> {job.jobName}</p>
            <p><strong>📝 Açıklama:</strong><br /> {job.description}</p>
            <p className="text-muted">
              📅 Yayın Tarihi: {new Date(job.createDate).toLocaleDateString("tr-TR")}
            </p>
          </div>
          <div className="d-flex flex-column flex-sm-row gap-3 mt-4 justify-content-center">
            <button className="btn btn-dark px-4" onClick={handleApplyClick}>
              Başvur
            </button>
            <Link to="/" className="btn btn-outline-secondary px-4">
              Geri Dön
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default JobDetails;
