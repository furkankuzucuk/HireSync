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

  if (!job) return <div className="container mt-5">Yükleniyor...</div>;

  return (
    <div className="job-details container py-5">
      <div className="card shadow border-0">
        <div className="card-header bg-primary text-white text-center py-3">
          <h3 className="mb-0">{job.title}</h3>
        </div>
        <div className="card-body p-4">
          <p><strong>Departman:</strong> {job.departmentName}</p>
          <p><strong>Pozisyon:</strong> {job.jobName}</p>
          <p><strong>Açıklama:</strong> {job.description}</p>
          <p className="text-muted">Yayın Tarihi: {new Date(job.createDate).toLocaleDateString()}</p>
          <div className="d-flex flex-column flex-sm-row gap-2 mt-4">
            <button className="btn btn-success" onClick={handleApplyClick}>
              Başvur
            </button>
            <Link to="/" className="btn btn-secondary">
              Geri Dön
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default JobDetails;
