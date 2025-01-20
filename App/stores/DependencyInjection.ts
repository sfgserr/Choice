import React from 'react';
import {ObjectGraph} from '../services/ObjectGraph.ts';

export function useDependency<T>(serviceName: string): T {
  return React.useMemo(() => ObjectGraph.resolve<T>(serviceName), [serviceName]);
}
