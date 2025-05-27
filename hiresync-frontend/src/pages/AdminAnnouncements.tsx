import React, { useState, useEffect } from "react";
import axios from "axios";
import "../css/AdminAnnouncementManager.css";

interface Announcement {
  announcementId: number;
  title: string;
  content: string;
  createdDate: string;
}

const AdminAnnouncementManager = () => {
  const [announcements, setAnnouncements] = useState<Announcement[]>([]);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState<string | null>(null);
  const token = localStorage.getItem("token");

  useEffect(() => {
    fetchAnnouncements();
  }, []);

  const fetchAnnouncements = async () => {
    setLoading(true);
    try {
      const response = await axios.get("/api/announcements", {
        headers: { Authorization: `Bearer ${token}` },
      });
      setAnnouncements(response.data);
    } catch {
      setMessage("❌ Duyurular alınırken hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  const handleCreate = async () => {
    if (!title.trim() || !content.trim()) {
      setMessage("⚠️ Başlık ve içerik zorunludur.");
      return;
    }

    setLoading(true);
    try {
      await axios.post("/api/announcements", {
        title,
        content,
        createdDate: new Date().toISOString(),
      }, {
        headers: { Authorization: `Bearer ${token}` },
      });

      setTitle("");
      setContent("");
      setMessage("✅ Duyuru başarıyla oluşturuldu.");
      await fetchAnnouncements();
    } catch {
      setMessage("❌ Oluşturma sırasında hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number) => {
    setLoading(true);
    try {
      await axios.delete(`/api/announcements/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      setAnnouncements((prev) => prev.filter((a) => a.announcementId !== id));
      setMessage("🗑️ Duyuru silindi.");
    } catch {
      setMessage("❌ Silme sırasında hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="announcement-container">
      <h2 className="section-title">📢 Duyuru Yönetimi</h2>

      <div className="create-box shadow-sm">
        <h4>Yeni Duyuru Oluştur</h4>
        <input
          type="text"
          placeholder="Başlık"
          className="form-control"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <textarea
          placeholder="İçerik"
          className="form-control mt-2"
          rows={4}
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />
        <button className="btn btn-primary mt-2" onClick={handleCreate} disabled={loading}>
          {loading ? "Yaratılıyor..." : "Duyuru Oluştur"}
        </button>
      </div>

      {message && <div className="alert alert-info mt-3">{message}</div>}

      <div className="announcement-list mt-4">
        {announcements.map((a) => (
          <div key={a.announcementId} className="announcement-item card mb-3">
            <div className="card-body">
              <h5 className="card-title">{a.title}</h5>
              <p className="card-text">{a.content}</p>
              <small className="text-muted">
                Oluşturulma: {new Date(a.createdDate).toLocaleString("tr-TR")}
              </small>
              <div className="text-end mt-2">
                <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(a.announcementId)}>
                  Sil
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default AdminAnnouncementManager;
