import { StrictMode, useEffect } from 'react'
import { createRoot } from 'react-dom/client'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import '../index.scss'
import Router from './components/Router';
import { checkToken } from './actions/authActions';
import { Provider, TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import store from './store';
import type { AppDispatch, RootState } from './store';

export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

const AppInitializer: React.FC = () => {
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(checkToken());
    }, [dispatch]);

    return <Router />;
};

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <Provider store={store}>
            <AppInitializer />
        </Provider>
    </StrictMode>,
  )
