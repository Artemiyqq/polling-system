import { BrowserRouter, Route, Routes } from "react-router-dom"
import LogIn from "./screens/log-in/LogIn";

const Router = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/log-in" element={<LogIn />} />
            </Routes>
        </BrowserRouter>
    )
}

export default Router;