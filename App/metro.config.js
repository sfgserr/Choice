const {getDefaultConfig, mergeConfig} = require('@react-native/metro-config');
const {
    wrapWithReanimatedMetroConfig,
} = require('react-native-reanimated/metro-config');
/**
 * Metro configuration
 * https://reactnative.dev/docs/metro
 *
 * @type {import('metro-config').MetroConfig}
 */
const config = mergeConfig(getDefaultConfig(__dirname), {resetCache: true});

module.exports = wrapWithReanimatedMetroConfig(config);
