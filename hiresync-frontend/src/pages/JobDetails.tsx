import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import axios from "axios";

const JobDetails = () => {
  const { id } = useParams();
  const [job, setJob] = useState<any>(null);

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
          <hr />
          <p className="text-muted">Yayın Tarihi: {new Date(job.createDate).toLocaleDateString()}</p>
          <Link to="/" className="btn btn-secondary mt-3">Geri Dön</Link>
        </div>
      </div>
    </div>
  );
};

export default JobDetails;
