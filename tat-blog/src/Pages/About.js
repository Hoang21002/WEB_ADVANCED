import React, { useEffect } from "react";

const About = () => {
    useEffect(() => {
        document.title = 'Trang chủ';
    }, []);

    return (
        <h1>
            Đây là trang giới thiệu
        </h1>
    );
}

export default About;