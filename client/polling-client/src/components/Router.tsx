import { BrowserRouter, Route, Routes } from "react-router-dom"
import LogIn from "./screens/log-in/LogIn";
import Register from "./screens/register/Register";

const Router = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/log-in" element={<LogIn />} />
                <Route path="/register" element={<Register />} />
            </Routes>
        </BrowserRouter>
    )
}

export default Router;