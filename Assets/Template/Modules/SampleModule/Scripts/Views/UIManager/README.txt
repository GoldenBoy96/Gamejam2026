A UI Manager will initialize UI follow a flow; then manage which to show and which to hide

This should include some Layer:
- Base: Only 1 of UIs in this layer should be shown at a time. This layer order is at the bottom
- Tab Group: a Tab Group contain many child Tab, only 1 tab in a group should be show at a time. The layer order is at mid
- Pop up: Multi UIs in this layer can be show at the same time. The order should be top, the local order should be base on which pop up appear first