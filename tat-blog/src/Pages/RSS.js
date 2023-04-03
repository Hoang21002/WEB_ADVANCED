import React, { useEffect } from "react";

const RSS = () => {
    useEffect(() => {
        document.title = 'rss';
    }, []);

    return (
        <h1>
            Đây là trang RSS
        </h1>
    );
}

export default RSS;