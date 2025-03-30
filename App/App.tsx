import * as React from 'react';
import Root from './Root.tsx';

function App(): React.JSX.Element {
  console.log(process.env.MINIO_URL);

  return <Root/>;
}

export default App;
