import React from 'react';
import styles from './App.module.css';
import { Header } from './Header';
import { Homepage } from './Homepage';

function App() {
  return (
    <div className={styles.container}>
      <Header />
      <Homepage />
    </div>
  );
}

export default App;
