import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import "../css/LandingPage.css";
import logo from "../image/hiresync.jpeg";

const getIconByJobTitle = (title: string) => {
  const lower = title.toLowerCase();
  if (lower.includes("developer") || lower.includes("yazılım")) return "👨‍💻";
  if (lower.includes("designer") || lower.includes("tasarım")) return "🎨";
  if (lower.includes("manager") || lower.includes("yönetici")) return "🧑‍💼";
  if (lower.includes("analyst") || lower.includes("analist")) return "📊";
  return "🛠️";
};

const LandingPage = () => {
  const [jobListings, setJobListings] = useState<any[]>([]);

  useEffect(() => {
    const fetchJobListings = async () => {
      try {
        const response = await axios.get("http://localhost:5065/api/joblists");
        setJobListings(response.data);
      } catch (error) {
        console.error("İş ilanları alınamadı:", error);
      }
    };
    fetchJobListings();
  }, []);

  return (
    <div className="container-fluid p-0 lp-container bg-white">
      <header className="bg-white border-bottom py-3">
        <div className="container d-flex align-items-center justify-content-between">
          <img src={logo} alt="HireSync Logo" style={{ height: "70px" }} />
          <Link to="/login" className="btn btn-login">
            Giriş Yap
          </Link>
        </div>
      </header>

      <section className="hero text-white text-center py-5">
        <div className="container">
          <h1 className="display-5 fw-bold">
            HireSync - İnsan Kaynakları Platformu
          </h1>
        </div>
      </section>

      <section className="job-listings py-5">
        <div className="container">
          <h2 className="section-title"> Açık Pozisyonlar</h2>
          <div className="row">
            {jobListings.length > 0 ? (
              jobListings.map((job, index) => (
                <div
                  className="col-md-6 col-lg-4 mb-4 job-card-animate"
                  key={job.jobListId}
                  style={{ animationDelay: `${index * 0.15}s` }}
                >
                  <div className="job-card-custom shadow-sm">
                    <div className="job-card-header d-flex align-items-center mb-3">
                      <div className="job-icon-alt">
                        {getIconByJobTitle(job.title)}
                      </div>
                      <div className="job-title">
                        <h5 className="fw-bold m-0">{job.title}</h5>
                        <small className="text-muted">
                          {new Date(job.createDate).toLocaleDateString("tr-TR")}
                        </small>
                      </div>
                    </div>
                    <p className="job-description text-muted">
                      {job.description?.slice(0, 100)}...
                    </p>
                    <div className="text-end">
                      <Link
                        to={`/job-details/${job.jobListId}`}
                        className="btn btn-sm btn-dark rounded-pill px-3"
                      >
                        Detayları Gör
                      </Link>
                    </div>
                  </div>
                </div>
              ))
            ) : (
              <p className="text-center">Henüz iş ilanı yok.</p>
            )}
          </div>
        </div>
      </section>

      <footer className="text-center py-3 text-muted small">
        HireSync
      </footer>
    </div>
  );
};

export default LandingPage;
