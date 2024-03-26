const { defaults } = require('babel-jest');

module.exports = {
  transform: {
    ...defaults.transform,
    '^.+\\.(js|jsx|ts|tsx)$': 'babel-jest',
  },
};